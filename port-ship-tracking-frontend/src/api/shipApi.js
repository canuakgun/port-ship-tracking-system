import axiosInstance from "./axiosInstance";

export const getShips = () => axiosInstance.get("/ships");
export const getShipById = (id) => axiosInstance.get(`/ships/${id}`);
export const createShip = (dto) => axiosInstance.post("/ships", dto);
export const updateShip = (id, dto) => axiosInstance.put(`/ships/${id}`, dto);
export const deleteShip = (id) => axiosInstance.delete(`/ships/${id}`);