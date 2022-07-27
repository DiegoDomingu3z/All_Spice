<template>
  <div>this working?</div>
</template>


<script>
import { onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { recipesService } from '../services/RecipesService'
import { AppState } from '../AppState'
export default {
  setup() {
    onMounted(async () => {
      try {
        await recipesService.getMyRecipes()
        logger.log(AppState.myRecipes)
      } catch (error) {
        logger.log(error)
        Pop.toast(error.message)
      }
    })
    return {

    }
  }
}
</script>


<style lang="scss" scoped>
</style>