import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService {
    async getRecipeIngredients(id) {
        const res = await api.get(`api/recipes/${id}/ingredients`)
        logger.log(res.data, '[ingredients]')
        AppState.activeIngredients = res.data
        logger.log(AppState.activeIngredients, 'ingredients in AppState')
    }
}


export const ingredientsService = new IngredientsService()