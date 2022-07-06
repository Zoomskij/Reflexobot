<template>
    <div>
        <div style="display:flex; justify-content:space-between">
            <span><b>Курсы</b></span>
            <el-button type="primary" size="small" >Создать новый курс</el-button>
        </div>

        <div v-for="course in courses" style="padding-left:10px; display: flex;">
            <course :course="course"></course>
        </div>
    </div>
</template>

<script>
    import Course from "~/js/components/course.vue";
    export default {
        name: 'courses',
        components: {Course},
        data() {
            return {
               courses: []
            }
        },
        computed: {

        },
        methods: {
            getCourses: function () {
                var self = this;
                this.$axios.get('/courses')
                    .then(function (response) {
                        self.courses = response.data;
                        //if (self.courses !== null && self.courses) {
                        //    self.getLessons('8B7AFF9B-E2D0-494F-85BD-D29F96C6AB65');
                        //}
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        },
        created() {

        },
        mounted() {
            this.getCourses();
        }
    }
</script>

<style>
    .clearfix:before,
    .clearfix:after {
        display: table;
        content: "";
    }

    .clearfix:after {
        clear: both
    }
</style>