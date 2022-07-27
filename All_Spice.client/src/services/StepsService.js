import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService {

    async getRecipeSteps(id) {
        const res = await api.get(`api/recipes/${id}/steps`)
        logger.log(res.data)
        AppState.activeSteps = res.data
    }

    async createStep(stepData) {
        const res = await api.post('api/steps', stepData)
        logger.log(res.data)
        AppState.activeSteps.push(res.data)
    }

    async deleteStep(id) {
        const res = await api.delete('api/steps/' + id)
        logger.log(res.data)
        AppState.activeSteps = AppState.activeSteps.filter(s => s.id != id)

    }

    async editStep(id, newPosition, newBody) {
        const res = await api.put('api/steps/' + id, { stepPosition: newPosition, body: newBody })
        logger.log(res.data)
    }

}

export const stepsService = new StepsService()