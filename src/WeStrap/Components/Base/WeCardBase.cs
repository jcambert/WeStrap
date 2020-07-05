﻿using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeCardBase : WeTag
    {

        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add($"color-{Color.ToDescriptionString()}",Color!=Color.None)
                
                .Add(GetClass());
        }

        protected override string Tag
        {
            get => CardType switch
            {
                CardType.Image => "img",
                CardType.Title => "h5",
                CardType.Subtitle => "h6",
                CardType.Text => "p",
                CardType.Link => "a",
                CardType.Header => HeadingSize.ToDescriptionString(),
                _ => "div"
            };
            set => base.Tag = value;
        }
        [Parameter] public CardType CardType { get; set; } = CardType.Card;
        [Parameter] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.None;
        [Parameter] public Alignment Alignment { get; set; } = Alignment.Left;
        [Parameter] public HeadingSize HeadingSize { get; set; } = HeadingSize.None;
        [Parameter] public bool IsInverse { get; set; }
        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public bool IsOutline { get; set; }
        private string GetImageVerticalAlignmnet()
        {
            return VerticalAlignment switch
            {
                VerticalAlignment.Top => "-top",
                VerticalAlignment.Bottom => "-bottom",
                _ => null
            };
        }

        private string GetCardAlignmnet()
        {
            return Alignment switch
            {
                Alignment.Center => "text-center",
                Alignment.Right => "text-right",
                _ => null
            };
        }

        private string GetCardColor()
        {
            if (Color == Color.None) { return null; }
            var textColor = Color == Color.Light || IsOutline ? null : "text-white";
            var prefix = IsOutline ? "border" : "bg";
            return $"{textColor} {prefix}-{Color.ToDescriptionString()}";
        }

        private string GetTextColor()
        {
            return IsOutline ? null : Color == Color.None || Color == Color.Light ? null : $"text-{Color.ToDescriptionString()}";
        }

        private string GetClass()
        {
            return CardType switch
            {
                CardType.Body => $"card-body {GetTextColor()}",
                CardType.Card => $"card {GetCardColor()} {GetCardAlignmnet()} {(IsInverse ? "bg-dark text-white" : null)}",
                CardType.Image => $"card-img{GetImageVerticalAlignmnet()}",
                CardType.Title => "card-title",
                CardType.Subtitle => "card-subtitle mb-2 text-muted",
                CardType.Link => "card-link",
                CardType.Header => "card-header",
                CardType.Footer => "card-footer",
                CardType.ImageOverlayContainer => "card-img-overlay-container",
                CardType.ImageOverlay => "card-img-overlay",
                CardType.Group => "card-group",
                CardType.Deck => "card-deck",
                CardType.Columns => "card-columns",
                _ => null
            };
        }
    }
}
