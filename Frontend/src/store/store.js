import {observable, action, makeObservable} from 'mobx';
import {toast} from "react-toastify";

class AuthStore {
    isAuthenticated = !!localStorage.getItem('token');
    login = (token) => {
        toast.info("You have successfully logged in, " /* + localStorage.getItem("Username") */,
            {toastId: "LogInInfo"})
        this.isAuthenticated = true;
        localStorage.setItem('token', token);
    }
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