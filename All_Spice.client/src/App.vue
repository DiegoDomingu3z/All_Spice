<template>
  <header class="bg-white">
    <Navbar />
  </header>
  <main class="bg-white">
    <router-view />
  </main>
  <Modal id="recipe">
    <template #header>New Recipe</template>
    <template #body><RecipeForm /></template>
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
