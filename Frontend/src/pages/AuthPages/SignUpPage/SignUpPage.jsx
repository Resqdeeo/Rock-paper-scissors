import  { useState } from 'react';
import './SignUpPage.css';
import useAuth from "../../../hooks/useAuth.js";
import {useNavigate} from "react-router-dom";

const SignUpPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const { loading, register } = useAuth();
    const navigate = useNavigate();

    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!username || !password || !confirmPassword) {
            setError('Please fill in all fields');
            return;
        } else if (password !== confirmPassword) {
            setError('Passwords do not match');
            return;
        } else { setError('') }

        const userData = {
            username: username,
            password: password,
        };

        const response = await register(userData);
        if (response.error) {
            setError(response.error);
        } else {
            navigate("/gamelist")
        }
    };

    return (
        <div className="sign-up-container">
            <div className="form-box">
                <h2>Sign up</h2>
                <form onSubmit={handleSubmit}>
                    <div className="form-group-sign-up">
                        <label htmlFor="username">Username</label>
                        <input
                            type="text"
                            id="username"
                            name="username"
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />
                    </div>
                    <div className="form-group-sign-up">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <div className="form-group-sign-up">
                        <label htmlFor="confirmPassword">Confirm Password</label>
                        <input
                            type="password"
                            id="confirmPassword"
                            name="confirmPassword"
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                        />
                    </div>
                    {error && <p className="error-message">{error}</p>}
                    <button type="submit" className="submit-button">
                        {loading ? "Wait..." : "Continue"}
                    </button>
                </form>
                <p className="sign-in-link">Already have an account? <a href="/signin">Sign in</a></p>
            </div>
        </div>
    );
};

export default SignUpPage;
