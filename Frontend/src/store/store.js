import {observable, action, makeObservable} from 'mobx';
import {toast} from "react-toastify";

class AuthStore {
    isAuthenticated = !!localStorage.getItem('token');
    login = (token) => {
        if (!token || typeof token !== "string") {
            console.error("Invalid token:", token);
            return;
        }
        toast.info("You have successfully logged in", { toastId: "LogInInfo" });
        this.isAuthenticated = true;
        console.log(token);
        console.log(token.toString());
        console.log("запись");
        localStorage.setItem('token', token.toString());
        console.log("Logged in", localStorage.getItem('token'));
    };
    logout = () => {
        toast.info("You have successfully logged out", {toastId: "LogOutInfo"})
        this.isAuthenticated = false;
        localStorage.removeItem('token');
        localStorage.removeItem('username');
    }
    constructor() { makeObservable(this, {
        isAuthenticated: observable,
        login: action,
        logout: action
    })
    }
}

const authStore = new AuthStore();

export default authStore;