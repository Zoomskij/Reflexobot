<template>
    <div>
        <el-card class="box-card">
            <div slot="header" class="clearfix">
                <span><b>{{course.name}}!</b></span>

            </div>
            <lesson v-for="lesson in lessons" :lesson="lesson"></lesson>
            <div v-for="lesson in lessons" :key="lessons.guid" class="text item">
                <lesson :lesson="lesson"></lesson>
            </div>
        </el-card>
    </div>
</template>

<script>
    import Lesson from "~/js/components/lesson.vue";
    export default {
        name: 'course',
        props: ['course'],
        components: {
            Lesson
        },
        data() {
            return {
               lessons: []
            }
        },
        computed: {

        },
        methods: {
            getLessons: function (courseGuid) {
                var self = this;
                this.$axios.get('/lessons/' + courseGuid)
                    .then(function (response) {
                        self.lessons = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
        },
        created() {

        },
        mounted() {
            this.getLessons(this.course.guid);
        }
    }
</script>

<style>
  
</style>