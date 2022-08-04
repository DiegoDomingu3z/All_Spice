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
        logger.log(AppState.myFavorites, 'favorites appState')
    }

    async deleteFavorite(id) {
        const res = await api.delete('api/favorites/' + id)
        logger.log(res.data)
        AppState.myFavorites = AppState.myFavorites.filter(f => f.favoriteId != id)
    }

}



export const favoritesService = new FavoritesService()