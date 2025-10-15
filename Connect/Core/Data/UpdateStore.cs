using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SAMS.Connect.Core.Models;

namespace SAMS.Connect.Core.Data;

public sealed class UpdateStore
    : ObservableObject
{
#region Data

    private readonly SortedDictionary<DateTime, LocalUpdate> _collection = [];
    private readonly ConcurrentDictionary<Department, int> _categoryFilterFrequency = [];
    private readonly HashSet<DateTime> _uniqueDates = [];
    private readonly HashSet<Department> _uniqueDepartments = [];

    private Department? _recommendedCategory;

#endregion

#region CRUD

    public void Insert(
        LocalUpdate instance
    ) {
        _collection[instance.CreatedAt] = instance;
        _uniqueDates.Add(instance.CreatedAt.Date);

        if (instance.UpdateCategory.HasValue) {
            _uniqueDepartments.Add(instance.UpdateCategory.Value);
        }
    }


    public IReadOnlyCollection<LocalUpdate> FetchAll() =>
        new ReadOnlyCollection<LocalUpdate>(_collection.Values.ToList());

#endregion

#region Helpers

    public void LogCategoryFilterUsage(Department category) {
        _categoryFilterFrequency.AddOrUpdate(
            key: category,
            addValue: 1,
            updateValueFactory: (_, count) => count + 1
        );

        _recommendedCategory = _categoryFilterFrequency
            .OrderByDescending((x) => x.Value)
            .First()
            .Key;
    }


    public Department? GetRecommendedCategory() => _recommendedCategory;


    public bool IsRecommended(LocalUpdate instance) {
        return (
            instance.UpdateCategory.HasValue && _recommendedCategory.HasValue &&
            _recommendedCategory.Value == instance.UpdateCategory.Value
        );
    }


    public IReadOnlySet<Department> GetAvailableCategories() =>
        _uniqueDepartments;


    public IReadOnlySet<DateTime> GetUniqueDates() =>
        _uniqueDates;

#endregion

#region Dev

    public void Seed() {
        _collection.Clear();
        var currentTime = DateTime.Parse("2025-10-15 13:18:34Z");
        var sampleUpdates = new[] {
            new LocalUpdate {
                UpdateHeading = "Power Outage Notice",
                UpdateCategory = Department.Electrical,
                UpdateMessage =
                    "Attention Central District residents: A **critical, scheduled power interruption** is set for this Friday, October 17th, from 9:00 AM to 3:00 PM. This necessary outage is to facilitate major upgrades to the primary substation infrastructure serving your area. Our **Electrical Department** engineers will be replacing aging transformers and installing smart grid technology to enhance long-term reliability and reduce future unscheduled outages. \n\nPlease be advised that during this six-hour window, all electrical service will be temporarily suspended. We strongly recommend unplugging sensitive electronic equipment beforehand to prevent potential damage when power is restored. Residents requiring life support equipment should ensure backup power systems are operational. \n\nWe apologize for the inconvenience this may cause and appreciate your understanding as we work to modernize our electrical network. The work is crucial for supporting the growing energy demands of the Central District and improving the overall resilience of the municipal power supply. Should the work be completed ahead of schedule, power will be restored immediately, but please plan for the full duration. For further inquiries or emergency contacts during the outage, please consult the dedicated **Electrical Department** hotline listed on the municipal website.",
                CreatedAt = currentTime.AddDays(-2)
            },
            new LocalUpdate {
                UpdateHeading = "Community Meeting",
                UpdateCategory = Department.Infrastructure,
                IsEvent = true,
                UpdateMessage =
                    "All citizens are invited to attend the **Annual Infrastructure Planning Meeting** this coming Monday, October 20th, at 6:30 PM in the City Hall Auditorium. This is a vital forum where the **Infrastructure Department** will present proposed capital projects for the upcoming fiscal year, covering essential areas like road resurfacing, bridge maintenance, public facility improvements, and long-term development strategies for transportation and utility corridors. \n\nThe agenda includes a presentation on the results of the recent city-wide road quality survey and a detailed look at the new urban green space initiative. Following the presentation, there will be an extensive Q&A session, providing a direct opportunity for residents to voice concerns, suggest modifications to proposed plans, and offer feedback that will directly influence the prioritization of projects. \n\nWe encourage all neighborhood associations, local businesses, and individual property owners to participate. Your **community input is critical** to ensuring that our infrastructure investments are aligned with the actual needs and priorities of the people we serve. Detailed blueprints and project summaries will be available for review at the meeting. Secure your free RSVP through the municipal event portal, and let your voice be heard on the future physical framework of our city.",
                CreatedAt = currentTime.AddDays(5)
            },
            new LocalUpdate {
                UpdateHeading = "Water Supply Update",
                UpdateCategory = Department.Water,
                UpdateMessage =
                    "Excellent news! The emergency **water main repair** initiated yesterday in the **Western Sector** has been successfully completed and tested ahead of the projected timeline. The **Water Department** crews worked tirelessly overnight to fully isolate and replace a critical segment of the aging distribution line that caused the disruption. \n\nAs of 1:00 PM today, full water pressure has been completely restored to all affected residential and commercial properties in the Western Sector. We appreciate the immense patience and cooperation demonstrated by all residents during this necessary repair. \n\nFollowing any major water service interruption, you may notice some temporary cloudiness or discolouration in your tap water due to trapped air or sediment stirring up in the lines. This is typically harmless. We recommend running your cold water tap for a few minutes until it runs clear. If the problem persists past 24 hours, or if you detect an unusual odor, please immediately contact the **Water Department**'s non-emergency line. We are committed to maintaining the highest quality of service and potable water for all citizens. Thank you again for your understanding while our teams ensured the structural integrity and safety of the local water system.",
                CreatedAt = currentTime.AddHours(-6)
            },
            new LocalUpdate {
                UpdateHeading = "Road Closure",
                UpdateCategory = Department.Sanitation,
                IsEvent = true,
                UpdateMessage =
                    "URGENT PUBLIC SAFETY ALERT: A major section of **Main Street**, between Oak Avenue and Elm Street, is currently closed for all vehicular and pedestrian traffic due to an unforeseen incident requiring immediate **Sanitation Department** response. The closure is effective immediately and is anticipated to last for the next 48 hours. This action is necessary to safely manage and remove a spill of non-hazardous but highly disruptive commercial waste and to conduct emergency repairs to an underlying sewage line potentially damaged by the incident. \n\n**Sanitation Department** crews are on-site with specialized equipment to expedite the clean-up and repair process to restore public access as quickly as possible. All motorists are strongly advised to follow the official detour signs posted at major intersections—alternative routes via Central Boulevard are recommended. Please avoid the area entirely if possible to allow our emergency teams unhindered access to the site. \n\nWe prioritize the health and safety of our community, and this temporary closure is essential to prevent environmental contamination and further infrastructure damage. Regular updates on the progress and estimated time of reopening will be provided via this channel. We apologize for the significant disruption to your daily commute and thank you for your compliance with the temporary closure measures.",
                CreatedAt = currentTime.AddDays(-1)
            },
            new LocalUpdate {
                UpdateHeading = "Service Center Opening",
                UpdateCategory = Department.Water,
                IsEvent = true,
                UpdateMessage =
                    "Mark your calendars! The **Water Department** is thrilled to announce the official **Grand Opening Ceremony** for our **New Municipal Service Center** this Saturday, October 18th, at 10:00 AM. Located conveniently at 450 City Circle, this modern facility is designed to significantly improve service delivery and accessibility for all water-related citizen needs. \n\nThe new center will feature dedicated stations for customer support, including in-person bill payment, assistance with consumption inquiries, resolution of billing discrepancies, and processing of new service applications and permit requests for major plumbing work. The inaugural event will include a ribbon-cutting ceremony, a brief address from the Mayor and the Director of the **Water Department**, and guided tours of the facility to showcase the technology and resources available to the public. \n\nLight refreshments will be served, and staff will be on hand to answer questions about water conservation programs and the city's future water quality initiatives. This center represents a major investment in public service, aimed at making interactions with the **Water Department** as efficient and transparent as possible. We look forward to welcoming the entire community to this milestone event!",
                CreatedAt = currentTime.AddDays(2)
            }
        };

        foreach (var instance in sampleUpdates) {
            Insert(instance);
        }
    }

#endregion
}
