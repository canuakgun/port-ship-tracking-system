import axiosInstance from "./axiosInstance";

export const getCrewMembers = () => axiosInstance.get("/crewmembers");
export const getCrewMemberById = (id) => axiosInstance.get(`/crewmembers/${id}`);
export const createCrewMember = (dto) => axiosInstance.post("/crewmembers", dto);
export const updateCrewMember = (id, dto) => axiosInstance.put(`/crewmembers/${id}`, dto);
export const deleteCrewMember = (id) => axiosInstance.delete(`/crewmembers/${id}`);