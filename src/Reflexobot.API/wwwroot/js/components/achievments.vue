<template>
    <div>
        <el-card class="box-card achievments">
            <h3>Мои достижения</h3>
            <el-tooltip v-for="achievment in achievments" class="item-tooltip" effect="light" :content='achievment.description' placement="top-start">
                <img :src='achievment.img'
                        height="60px"
                        weight="60px" />    
            </el-tooltip>
        </el-card>
    </div>
</template>

<script>
    export default {
        name: 'achievments',

        data() {
            return {
                achievments: []
            }
        },
        computed: {

        },
        methods: {
            getAchievments: function () {
                var self = this;
                this.$axios.get('/achievment')
                    .then(function (response) {
                        self.achievments = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
        },
        created() {

        },
        mounted() {
            this.getAchievments();
        }
    }
</script>

<style>
    .item-tooltip {
        margin: 4px;
    }
    .clearfix:before,
    .clearfix:after {
        display: table;
        content: "";
    }

    .clearfix:after {
        clear: both
    }
</style>