using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorFront.Resources
{
    class BackgroundCanvas : IDrawable
    {
        private Microsoft.Maui.Graphics.IImage _image;
        private float _time = 0;

        // Параметры искажения
        private float _waveFrequency = 10f;
        private float _waveAmplitude = 5f;
        private float _distortionSpeed = 2f;

        public BackgroundCanvas()
        {
            // Загружаем изображение
            LoadImage();
        }

        private async void LoadImage()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("CalculatorFront.Resources.Images.dotnet_bot.png")) // Replace with your image path
            {
                if (stream != null)
                {
                    _image = PlatformImage.FromStream(stream);
                }
            }

        }




        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (_image == null) return;

            canvas.SaveState();

            // Очищаем холст
            canvas.FillColor = Colors.Black;
            canvas.FillRectangle(dirtyRect);

            // Применяем искажение
            ApplyDistortion(canvas, dirtyRect);

            // Рисуем изображение
            canvas.DrawImage(_image, dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);

            canvas.RestoreState();
        }

        private void ApplyDistortion(ICanvas canvas, RectF rect)
        {
            // Простая синусоидальная волна
            float distortion = (float)Math.Sin(_time * _distortionSpeed) * _waveAmplitude;

            // Искажение по вертикали (волна)
            for (float y = 0; y < rect.Height; y += 1)
            {
                float wave = (float)Math.Sin(y / rect.Height * Math.PI * _waveFrequency + _time) * _waveAmplitude;
                canvas.Translate(wave, 0);
                // Здесь можно рисовать по строкам
            }

            // Или используем матрицу преобразования
            canvas.Rotate(distortion * 0.5f, rect.Center.X, rect.Center.Y);
            canvas.Scale(1 + distortion * 0.01f, 1 - distortion * 0.005f);
        }


    }
}
