import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService {

    async getRecipeSteps(id) {
        const res = await api.get(`api/recipes/${id}/steps`)
        logger.log(res.data)
        AppState.activeSteps = res.data
    }


}

export const stepsService = new StepsService()