import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {

    async getAll() {
        const res = await api.get('api/recipes')
        logger.log(res.data, "[Recipe Res]")
        AppState.recipes = res.data
    }

    async createRecipe(recipeData) {
        const res = await api.post("api/recipes", recipeData)
        logger.log(res.data)
        AppState.recipes.unshift(res.data)
    }


}


export const recipesService = new RecipesService()