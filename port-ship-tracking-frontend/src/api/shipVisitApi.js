import axiosInstance from "./axiosInstance";

export const getVisits = () => axiosInstance.get("/shipvisits");
export const getVisitById = (id) => axiosInstance.get(`/shipvisits/${id}`);
export const createVisit = (dto) => axiosInstance.post("/shipvisits", dto);
export const updateVisit = (id, dto) => axiosInstance.put(`/shipvisits/${id}`, dto);
export const deleteVisit = (id) => axiosInstance.delete(`/shipvisits/${id}`);