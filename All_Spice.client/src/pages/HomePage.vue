<template>
  <div class="container-fluid">
    <div class="row mb-5">
      <Recipes v-for="r in recipe" :key="r.id" :recipe="r" />
    </div>
  </div>
  <RecipeDetailedModal />
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { recipesService } from '../services/RecipesService'
import { AppState } from '../AppState'
import { favoritesService } from '../services/FavoriteService'
import { ingredientsService } from '../services/IngredientsService'
export default {
  setup() {
    onMounted(async () => {
      try {
        await recipesService.getAll()

      } catch (error) {
        Pop.toast(error.message)
        logger.log(error)
      }
    })
    return {
      recipe: computed(() => AppState.recipes),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
