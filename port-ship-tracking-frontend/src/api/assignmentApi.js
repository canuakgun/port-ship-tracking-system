import axiosInstance from "./axiosInstance";

export const getAssignments = () => axiosInstance.get("/shipcrewassignments");
export const getAssignmentById = (id) => axiosInstance.get(`/shipcrewassignments/${id}`);
export const createAssignment = (dto) => axiosInstance.post("/shipcrewassignments", dto);
export const deleteAssignment = (id) => axiosInstance.delete(`/shipcrewassignments/${id}`);