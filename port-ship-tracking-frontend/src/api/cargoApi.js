import axiosInstance from "./axiosInstance";

export const getCargoesByShipId = (shipId) => axiosInstance.get(`/cargoes/ship/${shipId}`);
export const getCargoById = (id) => axiosInstance.get(`/cargoes/${id}`);
export const createCargo = (dto) => axiosInstance.post("/cargoes", dto);
export const updateCargo = (id, dto) => axiosInstance.put(`/cargoes/${id}`, dto);
export const deleteCargo = (id) => axiosInstance.delete(`/cargoes/${id}`);