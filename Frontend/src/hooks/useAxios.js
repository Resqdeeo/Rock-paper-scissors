import axios from 'axios';

export const axiosToBackend = axios.create({
    baseURL: "http://localhost:8080/api",
    withCredentials: true,
    timeout: 30000
});

axiosToBackend.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${localStorage.getItem('token')}`;
    return config;
})

export default axiosToBackend;