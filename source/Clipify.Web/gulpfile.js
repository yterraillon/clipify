const gulp = require('gulp');

gulp.task('css', () => {
  const postcss = require('gulp-postcss');

  return gulp.src('./wwwroot/css/site.css')
    .pipe(postcss([
      require('precss'),
      require('tailwindcss'),
      require('autoprefixer')
    ]))
    .pipe(gulp.dest('./wwwroot/css/'));
});