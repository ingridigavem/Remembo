import { CreateMatterFormInputs } from "@/components/home-panel/matter/create-matter"
import api, { ResponseApi } from "@/lib/api"
import { createAsyncThunk } from "@reduxjs/toolkit"
import { normalize, schema } from 'normalizr'

const matterEntity = new schema.Entity('matters')

export const fetchMatters = createAsyncThunk(
    'matters/fetchAll',
    async () => {
        try{
            const response = await api.get<ResponseApi<Matter[]>>("/api/matter")
            const normalized = normalize<Matter>(response.data.data, [matterEntity])
            return normalized.entities.matters
        } catch(err){
            console.error(err)
            return
        }
    },
)

export const createMatter = createAsyncThunk(
    'matters/createMatter',
    async (payload: CreateMatterFormInputs, thunkApi) => {
        const response = await api.post<ResponseApi<Matter>>("/api/matter", payload)

        if (response.data.hasErrors) {
            return thunkApi.rejectWithValue(response.data.exceptionMessage ?? response.data.errors.join(","))
        }

        return response.data.data as Matter
    },
)
