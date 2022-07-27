import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {

    async getAll(query = {}) {
        const res = await api.get('api/recipes/', { params: query })
        logger.log(res.data, "[Recipe Res]")
        AppState.recipes = res.data
    }

    async getById(id) {
        const res = await api.get('api/recipes/' + id)
        logger.log(res.data)
        AppState.activeRecipe = res.data
        logger.log(AppState.activeRecipe, "[Active Recipe]")
    }

    async getMyRecipes() {
        await this.getAll
        AppState.myRecipes = AppState.recipes.filter(r => r.creatorId == AppState.account.id)
    }

    async searchRecipe(query = '') {
        AppState.query = query
        const res = await api.get(`api/recipes?query=${query}`)
        logger.log(res.data)
        AppState.recipes = res.data
    }



    async createRecipe(recipeData) {
        const res = await api.post("api/recipes", recipeData)
        logger.log(res.data)
        AppState.recipes.unshift(res.data)
    }


    async deleteRecipe(id) {
        const res = await api.delete('api/recipes/' + id)
        logger.log(res.data)
        AppState.recipes = AppState.recipes.filter(r => r.id != id)

    }





}


export const recipesService = new RecipesService()