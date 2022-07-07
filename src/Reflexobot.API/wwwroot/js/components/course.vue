<template>
    <div>
        <el-card class="box-card">
            <div style="display: flex; justify-content:space-between; vertical-align:central">
                <div>
                    <img :src="course.img" height="100" width="100" />
                </div>
                <div>
                    <div style="padding-top:10px">
                        <span><b>{{course.name}}</b></span>
                    </div>
                    <div style="padding-top:10px">
                        <span>{{course.description}}</span>
                    </div>
                    <div style="display:flex; justify-content: flex-end">
                        <el-button type="primary" icon="el-icon-edit" v-on:click="isEditWindow = true" circle></el-button>
                        <el-button type="danger" icon="el-icon-delete" @click="deleteCourse(course.guid)" circle></el-button>
                    </div>
                </div>
            </div>

            <el-dialog title="Редактировать курс" :visible.sync="isEditWindow">
                <el-input placeholder="Название курса" v-model="course.name"></el-input>
                <el-input placeholder="Описание курса" v-model="course.description" style="padding-top:10px"></el-input>
                <el-input placeholder="Url картинки" v-model="course.img" style="padding-top:10px"></el-input>

                <span slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="edit(course.guid)">Сохранить</el-button>
                </span>
            </el-dialog>
        </el-card>
    </div>
</template>

<script>
    import Lesson from "~/js/components/lesson.vue";
    export default {
        name: 'course',
        props: ['course'],
        event: 'change',
        components: {
            Lesson
        },
        data() {
            return {
                lessons: [],
                isEditWindow: false
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
            deleteCourse: function (courseGuid) {
                var self = this;
                this.$axios.delete('/courses/' + courseGuid).then(function (response) {
                    console.log(response);
                    self.$emit('listen');
                    //location.reload();
                }).catch(function (error) {
                    console.log(error);
                });
            },
            edit: function (courseGuid) {
                var self = this;
                this.$axios.put('/courses', this.course).then(function (response) {
                    console.log(response);
                    self.$emit('listen');
                    self.isEditWindow = false;
                }).catch(function (error) {
                    console.log(error);
                });
            }
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