import { CreateReviewFormInputs } from "@/components/home-panel/review";
import api, { ResponseApi } from "@/lib/api";
import { createAsyncThunk } from "@reduxjs/toolkit";
import { normalize, schema } from "normalizr";

const subjectEntity = new schema.Entity(
  "subjects",
  {},
  { idAttribute: "subjectId" }
);

export const fetchDashboard = createAsyncThunk(
  "dashboard/fetchAll",
  async () => {
    try {
      const response = await api.get<ResponseApi<Dashboard>>("/api/dashboard");
      const { data } = response.data;
      const normalized = normalize<SubjectWithContent>(
        data?.subjectDetailsList,
        [subjectEntity]
      );
      return {
        statistics: data?.statistics,
        subjects: normalized.entities.subjects,
      };
    } catch (err) {
      console.error(err);
      return;
    }
  }
);

export const createContent = createAsyncThunk(
  "dashboard/createContent",
  async (payload: CreateReviewFormInputs, thunkApi) => {
    const response = await api.post<ResponseApi<DetailedNewContent>>(
      "/api/content",
      payload
    );

    if (response.data.hasErrors) {
      return thunkApi.rejectWithValue(
        response.data.exceptionMessage ?? response.data.errors.join(",")
      );
    }

    return response.data.data as DetailedNewContent;
  }
);
