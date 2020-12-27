﻿using System;
using System.IO;
using System.Collections.Generic;
// using File;

namespace twitch_bot
{
    class Program
    {
        static private bool update_settings(string line, int count, Dictionary<string, string> settings)
        {
            if (string.IsNullOrWhiteSpace(line))
                return true;

            if (!line.Contains(":"))
            {
                Console.WriteLine($"FILE: Line {count} has no semicolon");
                return false;
            }

            string[] elements = line.Split(":", StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 2)
            {
                Console.WriteLine($"FILE: Got {elements.Length} elements on line {count} expected 2");
                return false;
            }

            string el1 = elements[0], el2 = elements[1];
            if (!settings.ContainsKey(el1))
            {
                Console.WriteLine($"FILE: No such '{el1}' in line {count} as property\n\nPossible:");
                foreach (var tmp in settings)
                    Console.WriteLine("- " + tmp.Key);

                return false;
            }

            count++;

            return true;
        }
        static private Dictionary<string, string> read_settings()
        {
            var settings = new Dictionary<string, string> {
                {"platform", "twitch" },
                {"channel", null},
                {"bot_name", "EMU_DS"},
                {"server", "irc.chat.twitch.tv"},
                {"port", "6667"},
                {"password", null},
            };

            System.IO.StreamReader file =
                new System.IO.StreamReader("settings");

            int count = 1;
            string line = null;
            while ((line = file.ReadLine()) != null)
            {
                if (!update_settings(line, count, settings))
                    return null;
            }

            return settings;
        }

        static void Main(string[] args)
        {
            if (!File.Exists("settings"))
            {
                Console.WriteLine("No 'settings' file found.");
                return;
            }

            var settings = read_settings();
            if (settings == null)
                return;


        }


    }
}