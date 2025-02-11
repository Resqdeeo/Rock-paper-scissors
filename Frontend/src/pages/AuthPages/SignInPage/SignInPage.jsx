import  { useState } from 'react';
import './SignInPage.css';
import {useNavigate} from "react-router-dom";
import useAuth from "../../../hooks/useAuth.js";

const SignInPage = () => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const { loading, login } = useAuth();
    const navigate = useNavigate();

    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!username || !password) {
            setError('Please fill in all fields');
            return;
        } else { setError('') }

        const userData = {
            username: username,
            password: password,
        };

        await login(userData).then((result) => {
            if (result.error) {
                setError(result.error);
            } else {
                navigate("/gamelist")
            }
        });
    };

    return (
        <div className="sign-in-container">
            <div className="form-box">
                <h2>Sign in</h2>
                <form onSubmit={handleSubmit}>
                    <div className="form-group-sign-in">
                        <label htmlFor="username">Username</label>
                        <input
                            type="text"
                            id="username"
                            name="username"
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />
                    </div>
                    <div className="form-group-sign-in">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    {error && <p className="error-message">{error}</p>}
                    <button type="submit" className="submit-button">
                        {loading ? "Wait..." : "Sign in"}
                    </button>
                </form>
                <p className="sign-up-link">Don't have an account? <a href="/signup">Sign up</a></p>
            </div>
        </div>
    );
};

export default SignInPage;
