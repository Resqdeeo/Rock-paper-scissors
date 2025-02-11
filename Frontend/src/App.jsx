import 'react';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import SignUpPage from "./pages/AuthPages/SignUpPage/SignUpPage.jsx";
import SignInPage from "./pages/AuthPages/SignInPage/SignInPage.jsx";
import GameListPage from "./pages/MainPages/GameListPage/GameListPage.jsx";
import GamePage from "./pages/MainPages/GamePage/GamePage.jsx";
function App() {

  return (
    <>
        <Router>
            <Routes>
                <Route path="/signup" element={<SignUpPage />} />
                <Route path="/signin" element={<SignInPage />} />
                <Route path="/gamelist" element={<GameListPage />} />
                <Route path="/" element={<GamePage />} />
            </Routes>
        </Router>
    </>
  )
}

export default App
