<template>
	<v-container>
		<v-row>
			<v-col cols="12">
				<h1 class="display-1 text-center">Leaderboard</h1>
			</v-col>
		</v-row>
		<v-row v-for="(game, index) in games" :key="index">
			<v-col cols="12">
				<v-card class="pa-3 ma-2">
					<v-row>
						<v-col cols="3" class="subtitle-1">Name: {{ game.name }}</v-col>
						<v-col cols="3" class="subtitle-1">Score: {{ game.score }}</v-col>
						<v-col cols="3" class="subtitle-1">Lives: {{ game.lives }}</v-col>
						<v-col cols="3" class="subtitle-1">Time: {{ formatTime(game.time) }}</v-col>
					</v-row>
				</v-card>
			</v-col>
		</v-row>
	</v-container>



</template>

<script setup lang="ts">
import Axios from 'axios';

const games = ref([]);

onMounted(() => {
	Axios.get('Leaderboard/getLeaderboard')
		.then((response) => {
			console.log(response.data);
			games.value = response.data;
		})
		.catch((error) => {
			console.log(error);
		});
});

const formatTime = (timeString: string): string => {
	const date = new Date(timeString);
	return date.toLocaleTimeString();
};

</script>
