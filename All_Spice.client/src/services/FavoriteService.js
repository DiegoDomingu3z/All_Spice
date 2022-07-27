import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService {

    async getFavorites() {
        const res = await api.get('account/favorites')
        logger.log(res.data)
        AppState.myFavorites = res.data
    }
    async favoriteRecipe(recipeId, accountId) {
        const res = await api.post("api/favorites", recipeId, accountId)
        logger.log(res.data)
        AppState.favorites.push(res.data)
        AppState.myFavorites.push(res.data)
        logger.log(AppState.favorites, 'favorites appState')
    }

}


export const favoritesService = new FavoritesService()