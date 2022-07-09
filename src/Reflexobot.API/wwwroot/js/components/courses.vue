<template>
    <div>
        <div style="display:flex; justify-content:space-between; padding-bottom:10px">
            <span><b>Курсы</b></span>
            <el-button type="primary" size="small" @click="isNewWindow = true">Создать новый курс</el-button>
        </div>

        <div v-for="course in courses" style="padding-left:10px; display: flex;">
            <course :course="course" @listen="getCourses()"></course>
        </div>

        <el-dialog title="Добавить курс" :visible.sync="isNewWindow">
            <el-input placeholder="Название курса" v-model="courseEntity.name"></el-input>
            <el-input placeholder="Описание курса" v-model="courseEntity.description" style="padding-top:10px"></el-input>
            <el-input placeholder="Url картинки" v-model="courseEntity.img" style="padding-top:10px"></el-input>

            <span slot="footer" class="dialog-footer">
                <el-button type="primary" @click="addCourse()">Добавить курс</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>
    import Course from "~/js/components/course.vue";
    export default {
        name: 'courses',
        components: {Course},
        data() {
            return {
                courses: [],
                isNewWindow: false,
                courseEntity: {
                    name: '',
                    description: '',
                    img: ''
                }
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
            },
            addCourse: function () {
                var self = this;
                this.$axios.post('/courses', this.courseEntity).then(function (response) {
                    console.log(response);
                    self.getCourses();
                    self.isNewWindow = false;
                }).catch(function (error) {
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