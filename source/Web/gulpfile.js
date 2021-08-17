const gulp = require('gulp');

gulp.task('css', () => {
  const postcss = require('gulp-postcss');
  //const rename = require('gulp-rename');

    return gulp.src('./Styles/site.css')
    .pipe(postcss([
      require('precss'),
      require('tailwindcss'),
      require('autoprefixer')
    ]))
    //.pipe(rename('site.css'))
    .pipe(gulp.dest('./wwwroot/css/'));
});