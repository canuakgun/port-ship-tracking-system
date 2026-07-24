import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "http://localhost:5054/api",
});

export default axiosInstance;