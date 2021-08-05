const gulp = require('gulp');

gulp.task('css', () => {
  const postcss = require('gulp-postcss');
  const rename = require('gulp-rename');

  return gulp.src('./wwwroot/css/style.css')
    .pipe(postcss([
      require('precss'),
      require('tailwindcss'),
      require('autoprefixer')
    ]))
    .pipe(rename('site.css'))
    .pipe(gulp.dest('./wwwroot/css/'));
});