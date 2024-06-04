<template>
    <NuxtLayout>
        <v-app>
        <v-container>
            <v-row>
            <v-col cols="12">
                <h1 class="display-1 text-center">Welcome to the Game!</h1>
                <v-btn @click="router.push('/game')">Start Game</v-btn>
            </v-col>
            </v-row>
        </v-container>
        </v-app>
    </NuxtLayout>
    
</template>

<script setup lang="ts">
const router = useRouter();
</script>
