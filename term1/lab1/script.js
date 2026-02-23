let users = JSON.parse(localStorage.getItem("users") || "[]");
export let currentUser = null;

export function setCurrentUser(user) {
    currentUser = user;
    broadcastState();
}

const BC_NAME = 'app-auth-channel';
const bc = new BroadcastChannel(BC_NAME);
let localVersion = Date.now();

export function broadcastState() {
    localVersion = Date.now();
    bc.postMessage({
        type: 'STATE_UPDATE',
        version: localVersion,
        payload: { users, currentUser }
    });
}

export const listeners = new Set();

export function subscribeAuth(fn) {
    listeners.add(fn);
}

function notifyAuth() {
    listeners.forEach(fn => fn({ currentUser, users }));
}

bc.onmessage = (ev) => {
    const msg = ev.data;
    if (!msg || msg.type !== 'STATE_UPDATE') return;
    if (msg.version === localVersion) return;

    users = msg.payload.users || [];
    localStorage.setItem("users", JSON.stringify(users));
    currentUser = msg.payload.currentUser || null;

    notifyAuth();
};

// ----- State machine (SA) -----
export class SA {
    static START = { A: 'A', N: 'N' };
    static COMMAND = { REG: 'REG', SIN: 'SIN', CAN: 'CAN', ADE: 'ADE', LIN: 'LIN', SOU: 'SOU', REJ: 'REJ' };
    static STATES = { A: SA.START.A, N: SA.START.N, S: 'S', R: 'R', SV: 'SV', RV: 'RV' };
    static TRANFUNC = {
        A: { SOU: 'N' },
        N: { SIN: 'S', REG: 'R' },
        S: { SIN: 'SV', CAN: 'N' },
        R: { REG: 'RV', CAN: 'N' },
        SV: { LIN: 'A', ADE: 'S' },
        RV: { SIN: 'S', REJ: 'R' }
    };

    constructor(startState) {
        this.state = SA.START[startState] != undefined ? startState : SA.START.A;
        this.subscribers = {};
    }

    on(state, callback) {
        if (!this.subscribers[state]) this.subscribers[state] = [];
        this.subscribers[state].push(callback);
    }

    emit(state, data) {
        if (this.subscribers[state]) this.subscribers[state].forEach(cb => cb(data));
    }

    RunCommand(command) {
        const next = SA.TRANFUNC[this.state]?.[command];
        console.log(this.state, ':RunCommand: command =', command, '--->', next);
        if (next !== undefined) {
            this.state = next;
            console.log('RunCommand: new state =', this.state);
            this.emit(this.state, SA.TRANFUNC[this.state]);
        }
        return { [this.state]: SA.TRANFUNC[this.state] };
    }
}

// ----- User functions -----
export function GetUserName(setA, setN) {
    if (currentUser && setA) {
        setA({ name: currentUser.name });
        return { status: 200 };
    } else {
        if (setN) setN();
        return { status: 401 };
    }
}

export function Registration(name, pass, sin, rej) {
    if (!name || !pass) {
        if (rej) rej();
        return false;
    }

    const exists = users.some(u => u.name === name);
    if (exists) {
        if (rej) rej();
        return false;
    }

    users.push({ name, pass });
    localStorage.setItem("users", JSON.stringify(users));
    broadcastState();
    if (sin) sin();
    return true;
}

export function SignIn(name, pass, lin, ade) {
    users = JSON.parse(localStorage.getItem("users") || "[]");
    const user = users.find(u => u.name === name && u.pass === pass);

    if (user) {
        currentUser = { name: user.name };
        localStorage.setItem("currentUser", JSON.stringify(currentUser));
        broadcastState();
        if (lin) lin();
        return true;
    } else {
        if (ade) ade();
        return false;
    }
}

export function SignOut(sou) {
    currentUser = null;
    broadcastState();
    if (sou) sou();
    return true;
}
