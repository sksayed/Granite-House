using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Granite_House.Extension
{
    public class SelectItemList
    {
        public string Value { get; internal set; }
        public string Text { get; internal set; }
        public bool Selected { get; internal set; }
    }
}