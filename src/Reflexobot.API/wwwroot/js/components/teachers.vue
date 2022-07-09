<template>
    <div>
        <div style="display:flex; justify-content: flex-start; padding-left:10px" v-for="teacher in teachers">
            <el-card class="box-card teacher">
                <img :src="teacher.img" height="150px" weight="150px" />
                <h2>{{teacher.name}}</h2>
            </el-card>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'teachers',
        data() {
            return {
                teachers: []
            }
        },
        computed: {

        },
        methods: {
            getTeachers: function () {
                var self = this;
                this.$axios.get('/teachers')
                    .then(function (response) {
                        self.teachers = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        },
        created() {

        },
        mounted() {
            this.getTeachers();
        }
    }
</script>

<style>
  
</style>