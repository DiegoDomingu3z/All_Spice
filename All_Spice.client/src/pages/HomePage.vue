<template>
  <div class="container-fluid">
    <div class="row mb-5">
      <Recipes v-for="r in recipe" :key="r.id" :recipe="r" />
    </div>
    <button
      type="button"
      data-bs-toggle="modal"
      data-bs-target="#recipe"
      class="btn btn-success fab"
    >
      <p class="fs-1">+</p>
    </button>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { recipesService } from '../services/RecipesService'
import { AppState } from '../AppState'
import { favoritesService } from '../services/FavoriteService'
export default {
  setup() {
    onMounted(async () => {
      try {
        await recipesService.getAll()
        await favoritesService.GetFavorites()

      } catch (error) {
        Pop.toast(error.message)
        logger.log(error)
      }
    })
    return {
      recipe: computed(() => AppState.recipes)
    }
  }
}
</script>


<style lang="scss" scoped>
.fab {
  height: 60px;
  width: 60px;
  border-radius: 50%;
  position: fixed;
  bottom: 3em;
  right: 1em;
  z-index: 2;
}
</style>
