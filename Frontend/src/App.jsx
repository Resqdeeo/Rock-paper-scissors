import 'react';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import SignUpPage from "./pages/AuthPages/SignUpPage/SignUpPage.jsx";
import SignInPage from "./pages/AuthPages/SignInPage/SignInPage.jsx";
function App() {

  return (
    <>
        <Router>
            <Routes>
                <Route path="/signup" element={<SignUpPage />} />
                <Route path="/signin" element={<SignInPage />} />
            </Routes>
        </Router>
    </>
  )
}

export default App
