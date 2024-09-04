import { toast } from "@/components/ui/use-toast"
import { createSlice } from "@reduxjs/toolkit"
import { createContent, fetchDashboard } from "./thunk"

export interface DashboardState {
    statistics: Statistics
    matters: {
        [key: string] : MatterWithContent
    }
}

const initialState: DashboardState = {
    statistics: {
        completedReviewsTotal: 0,
        completedContentTotal: 0,
        notCompletedContentTotal: 0
    },
    matters: {},
}

export const dashboardSlice = createSlice({
    name: 'dashboard',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchDashboard.fulfilled, (state, action) => {
            if(action.payload) {
                if(action.payload.statistics)
                    state.statistics = action.payload.statistics
                if(action.payload.matters)
                    state.matters = action.payload.matters
            }
        })
        builder.addCase(createContent.fulfilled, (state, { payload }) => {
            const newContentReview = payload.matter.contentReview

            state.matters[payload.matter.matterId] = {
                ...state.matters[payload.matter.matterId],
                contents: [
                    ...state.matters[payload.matter.matterId].contents,
                    {
                        ...newContentReview
                    }
                ]

            }

            toast({
                variant: "success",
                title: "Conteúdo criado com sucesso",
            })
        })
    },
})

export default dashboardSlice.reducer
