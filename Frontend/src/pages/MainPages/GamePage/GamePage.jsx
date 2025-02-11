import 'react';
import './GamePage.css';

const GamePage = ({ opponentUsername }) => {
    return (
        <div className="game-page-container">
            <div className="opponent-info">
                <h2>Opponent: {opponentUsername}</h2>
            </div>
            <div className="game-buttons">
                <button className="choice-button">Rock</button>
                <button className="choice-button">Scissors</button>
                <button className="choice-button">Paper</button>
            </div>
        </div>
    );
};

export default GamePage;
