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


    async createIngredient(ingredientData) {
        const res = await api.post('api/ingredients', ingredientData)
        logger.log(res.data)
        AppState.activeIngredients.push(res.data)
    }

    async editIngredient(id, newQuantity, newName) {
        const res = await api.put('api/ingredients/' + id, { quantity: newQuantity, name: newName })
        logger.log(res.data)
    }

    async deleteIngredient(id) {
        const res = await api.delete('api/ingredients/' + id)
        logger.log(res.data)
        AppState.activeIngredients = AppState.activeIngredients.filter(i => i.id != id)
    }
}


export const ingredientsService = new IngredientsService()