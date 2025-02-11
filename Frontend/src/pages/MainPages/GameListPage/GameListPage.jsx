import  { useState, useEffect } from 'react';
import './GameListPage.css';
import axiosToBackend from "../../../hooks/useAxios.js";

// // Пример данных игр
// const games = [
//     { id: 1, username: 'User1', createdAt: '2023-10-01T10:00:00Z', status: 'not_started' },
//     { id: 2, username: 'User2', createdAt: '2023-09-25T12:00:00Z', status: 'started' },
//     { id: 3, username: 'User3', createdAt: '2023-10-05T14:00:00Z', status: 'not_started' },
//     { id: 4, username: 'User4', createdAt: '2023-09-30T16:00:00Z', status: 'completed' },
//     { id: 5, username: 'User5', createdAt: '2023-09-25T12:00:00Z', status: 'started' },
//     { id: 6, username: 'User6', createdAt: '2023-10-05T14:00:00Z', status: 'not_started' },
//     { id: 7, username: 'User7', createdAt: '2023-09-30T16:00:00Z', status: 'completed' },
//     // Добавьте больше данных для тестирования
// ];

// // Пример данных рейтинга
// const ratings = [
//     { username: 'User1', rating: 1500 },
//     { username: 'User2', rating: 1600 },
//     { username: 'User3', rating: 1450 },
//     { username: 'User4', rating: 1700 },
//     { username: 'User5', rating: 1300 },
//     { username: 'User6', rating: 1550 },
//     { username: 'User7', rating: 1650 },
// ];

const GameListPage = () => {
    const [gameList, setGameList] = useState([]);
    const [showRatingModal, setShowRatingModal] = useState(false);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [maxRating, setMaxRating] = useState('');
    const [ratings, setRatings] = useState([]);
    const [page, setPage] = useState(0);
    const [size] = useState(8);

    useEffect(() => {
        fetchGames(); // Загружаем список игр при загрузке страницы
    }, []);

    useEffect(() => {
        fetchGames();
    }, [page]);


    const fetchRating = async () => {
        try {
            const response = await axiosToBackend.get('/Game/ratings');
            setRatings(response.data);
        } catch (error) {
            console.error('Error fetching ratings:', error);
        }
    };

    const fetchGames = async () => {
        try {
            const response = await axiosToBackend.get('/Game/games', {
                params: { page, size }
            });
            const sortedGames = response.data.sort((a, b) => {
                const dateDiff = new Date(b.createdAt) - new Date(a.createdAt);
                if (dateDiff !== 0) return dateDiff;
                return a.status.localeCompare(b.status);
            });
            setGameList(sortedGames);
        } catch (error) {
            console.error('Error fetching games:', error);
        }
    };

    const handleShowRatingModal = async () => {
        await fetchRating();
        setShowRatingModal(true);
    };

    const handleCloseRatingModal = () => {
        setShowRatingModal(false);
    };

    const handleCloseCreateModal = () => {
        setShowCreateModal(false);
    };

    const handleCreateGame = async () => {
        try {
            const creatorId = "3fa85f64-5717-4562-b3fc-2c963f66afa6"; // Example creatorId
            const payload = {
                creatorId,
                maxRating: parseInt(maxRating, 10) || 0
            };
            const response = await axiosToBackend.post('/Game/create', payload);
            console.log('Game created:', response.data);
            setShowCreateModal(false);
            fetchGames(); // Refresh game list after creation
        } catch (error) {
            console.error('Error creating game:', error);
        }
    };

    return (
        <div className="game-list-container">
            <h2 className="game-list-title">Game List</h2>
            <div className="game-grid">
                {gameList.map((game) => (
                    <div key={game.id} className="game-item">
                        <div className="game-info">
                            <p>ID: {game.id}</p>
                            <p>Creator: {game.username}</p>
                            <p>Created At: {new Date(game.createdAt).toLocaleString()}</p>
                            <p>Status: {game.status.charAt(0).toUpperCase() + game.status.slice(1).replace('_', ' ')}</p>
                        </div>
                        <button
                            className={`join-button`}
                        >
                            Join
                        </button>
                    </div>
                ))}
            </div>
            <div className="floating-buttons">
                <button className="create-button" onClick={() => setShowCreateModal(true)}>Create Game</button>
                <button className="rating-button" onClick={handleShowRatingModal}>Rating</button>
            </div>

            <div className="pagination-controls">
                <button onClick={() => setPage((prev) => Math.max(prev - 1, 0))} disabled={page === 0}>
                    Previous
                </button>
                <button onClick={() => setPage((prev) => prev + 1)}>
                    Next
                </button>
            </div>


            {showRatingModal && (
                <div className="modal-overlay">
                    <div className="modal-content">
                        <h2>Player Ratings</h2>
                        <ul className="rating-list">
                            {ratings.map((rating, index) => (
                                <li key={index} className="rating-item">
                                    <span>{rating.username}</span>
                                    <span>{rating.rating}</span>
                                </li>
                            ))}
                        </ul>
                        <button className="close-button" onClick={handleCloseRatingModal}>Close</button>
                    </div>
                </div>
            )}

            {showCreateModal && (
                <div className="modal-overlay">
                    <div className="modal-content">
                        <h2>Create New Game</h2>
                        <div className="form-group">
                            <label htmlFor="maxRating">Maximum Rating</label>
                            <input
                                type="number"
                                id="maxRating"
                                value={maxRating}
                                onChange={(e) => setMaxRating(e.target.value)}
                                required
                            />
                        </div>
                        <div className="modal-buttons">
                            <button className="create-button" onClick={handleCreateGame}>Create Game</button>
                            <button className="cancel-button" onClick={handleCloseCreateModal}>Cancel</button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default GameListPage;
