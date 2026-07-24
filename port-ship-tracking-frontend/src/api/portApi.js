import axiosInstance from "./axiosInstance";

export const getPorts = () => axiosInstance.get("/ports");
export const getPortById = (id) => axiosInstance.get(`/ports/${id}`);
export const createPort = (dto) => axiosInstance.post("/ports", dto);
export const updatePort = (id, dto) => axiosInstance.put(`/ports/${id}`, dto);
export const deletePort = (id) => axiosInstance.delete(`/ports/${id}`);