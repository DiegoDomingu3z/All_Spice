<template>
  <div class="row mb-5">
    <Favorites v-for="f in favorites" :key="f.id" :favorites="f" />
  </div>
  <RecipeDetailedModal />
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { favoritesService } from '../services/FavoriteService'
import { AppState } from '../AppState'
export default {
  setup() {
    onMounted(async () => {
      try {
        await favoritesService.getFavorites()
      } catch (error) {
        logger.log(error)
        Pop.toast(error.message)
      }
    })
    return {

      favorites: computed(() => AppState.myFavorites)

    }
  }
}
</script>


<style lang="scss" scoped>
</style>