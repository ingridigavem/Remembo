import { CreateReviewFormInputs } from "@/components/home-panel/review"
import api, { ResponseApi } from "@/lib/api"
import { createAsyncThunk } from "@reduxjs/toolkit"
import { normalize, schema } from 'normalizr'

const matterEntity = new schema.Entity('matters', {}, { idAttribute: "matterId"})

export const fetchDashboard = createAsyncThunk(
    'dashboard/fetchAll',
    async () => {
        try{
            const response = await api.get<ResponseApi<Dashboard>>("/api/dashboard")
            const { data } = response.data
            const normalized = normalize<MatterWithContent>(data?.matterDetailsList.matters, [matterEntity])
            return { statistics: data?.statistics, matters: normalized.entities.matters}
        } catch(err){
            console.error(err)
            return
        }
    },
)

export const createContent = createAsyncThunk(
    'dashboard/createContent',
    async (payload: CreateReviewFormInputs, thunkApi) => {
        const response = await api.post<ResponseApi<DetailedNewContent>>("/api/content", payload)

        if (response.data.hasErrors) {
            return thunkApi.rejectWithValue(response.data.exceptionMessage ?? response.data.errors.join(","))
        }

        return response.data.data as DetailedNewContent
    },
)
