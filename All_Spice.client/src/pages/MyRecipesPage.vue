<template>
  <div class="row">
    <MyRecipes v-for="r in recipe" :key="r.id" :recipe="r" />
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { recipesService } from '../services/RecipesService'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
export default {
  setup() {
    onMounted(async () => {
      try {
        await accountService.getMyRecipes()
        logger.log(AppState.myRecipes, "better load")
      } catch (error) {
        logger.log(error)
        Pop.toast(error.message)
      }
    })
    return {
      recipe: computed(() => AppState.myRecipes)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>