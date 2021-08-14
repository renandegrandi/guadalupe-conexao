using System;
using System.Collections.Generic;
using System.Text;

namespace Guadalupe.Conexao.App.Model
{
    public class Reading
    {
        #region Properties
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Reference { get; set; }

        #endregion

        #region Métodos Públicos

        public Reading SetMock() 
        {
            Date = DateTime.Now;
            Reference = "HB, 20 - 10";
            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam dignissim dolor diam, id egestas purus varius nec. Phasellus nec fermentum velit. Suspendisse diam risus, consequat a augue sit amet, condimentum tempor velit. Aliquam tempor tellus vel mi condimentum vehicula vitae et sapien. Praesent vulputate dignissim eros, quis eleifend tellus interdum id. Sed semper nisi elementum, ultricies mauris ut, elementum arcu. Cras commodo vel orci in tempus. Cras odio ipsum, auctor et feugiat scelerisque, mattis in augue. Etiam est nisl, dictum vitae maximus ac, eleifend in libero. Aliquam vehicula convallis dui, a tempus odio fringilla luctus." +
                "Quisque pulvinar quam at tellus auctor, vitae pretium augue pharetra.Morbi non auctor tellus. Aliquam placerat, elit varius cursus mollis, quam risus mollis elit, id maximus justo arcu consequat diam.In tristique vitae ipsum id porttitor. Praesent sed mi iaculis, vestibulum metus sit amet, interdum justo. Ut eros enim, sagittis ac lacinia eget, dapibus sodales diam. Vivamus malesuada neque at ex pulvinar suscipit.Vivamus tempus tempus quam, at efficitur arcu. Vivamus sollicitudin dignissim blandit. Vestibulum cursus urna ut risus iaculis, eu efficitur justo semper.Proin vel ante pretium, tincidunt est in, consequat nibh. Vestibulum hendrerit mi augue, vel lacinia augue volutpat condimentum. Suspendisse consectetur velit vitae mauris rutrum maximus.";

            return this;
        }

        #endregion

    }
}
