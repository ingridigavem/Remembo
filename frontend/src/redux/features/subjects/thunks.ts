import { CreateSubjectFormInputs } from "@/components/home-panel/subject/create-subject";
import api, { ResponseApi } from "@/lib/api";
import { createAsyncThunk } from "@reduxjs/toolkit";
import { normalize, schema } from "normalizr";

const subjectEntity = new schema.Entity("subjects");

export const fetchSubjects = createAsyncThunk("subjects/fetchAll", async () => {
  try {
    const response = await api.get<ResponseApi<Subject[]>>("/api/subject");
    const normalized = normalize<Subject>(response.data.data, [subjectEntity]);
    return normalized.entities.subjects;
  } catch (err) {
    console.error(err);
    return;
  }
});

export const createSubject = createAsyncThunk(
  "subjects/createSubject",
  async (payload: CreateSubjectFormInputs, thunkApi) => {
    const response = await api.post<ResponseApi<Subject>>(
      "/api/subject",
      payload
    );

    if (response.data.hasErrors) {
      return thunkApi.rejectWithValue(
        response.data.exceptionMessage ?? response.data.errors.join(",")
      );
    }

    return response.data.data as Subject;
  }
);
