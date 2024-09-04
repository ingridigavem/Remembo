import { toast } from "@/components/ui/use-toast"
import { createSlice } from "@reduxjs/toolkit"
import { createMatter, fetchMatters } from "./thunks"

export interface MattersState {
    matters: {
        [key: string] : Matter,
    }
}

const initialState: MattersState = {
    matters: {},
}

export const matterSlice = createSlice({
    name: 'matters',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchMatters.fulfilled, (state, action) => {
            if(action.payload)
                state.matters = action.payload
        })
        builder.addCase(createMatter.fulfilled, (state, {payload}) => {
            state.matters[payload.id] = payload
            toast({
                variant: "success",
                title: "Matéria criada com sucesso",
            })
        })
    },
})

export default matterSlice.reducer
