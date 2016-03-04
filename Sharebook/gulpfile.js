
var gulp = require('gulp'),
    aspnetk = require("gulp-aspnet-k");

gulp.task('default', function(cb) {
    return gulp.start('aspnet-run');
});

gulp.task('aspnet-run', aspnetk());