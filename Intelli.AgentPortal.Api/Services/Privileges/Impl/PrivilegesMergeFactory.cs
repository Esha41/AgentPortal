using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// Multiple Similar privileges merge factory.
    /// This factory class will merge privileges and returns distinct privileges based on the best available
    /// privilege setting.
    /// </summary>
    internal class PrivilegesMergeFactory
    {
        readonly List<RoleScreenDTO> _screenList = new();

        /// <summary>
        /// Adds the role screen dto to list
        /// </summary>
        /// <param name="dto">The dto.</param>
        internal void Add(RoleScreenDTO dto)
        {
            _screenList.Add(dto);
        }

        /// <summary>
        /// Gets the merged role screen dto list.
        /// </summary>
        /// <returns>A list of RoleScreenDTOS.</returns>
        internal List<RoleScreenDTO> GetMerged()
        {
            var list = new List<RoleScreenDTO>();
            for (int i = 0; i < _screenList.Count; i++)
            {
                int screenId = _screenList[i].ScreenId;
                list.Add(MergeIdentical(_screenList.FindAll(x => x.ScreenId == screenId)));

                _screenList.RemoveAll(x => x.ScreenId == screenId);
                i = -1;
            }
            return list;
        }

        /// <summary>
        /// Merges the identical role screen dtos.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>A RoleScreenDTO.</returns>
        private static RoleScreenDTO MergeIdentical(List<RoleScreenDTO> list)
        {
            // return if only 1 dto in list.
            if (list.Count == 1) return list[0];

            // Hold 1st screen to start with.
            var dto = list[0];

            // Merge Screen Priviliges.
            for (int i = 1; i < list.Count; i++)
            {
                dto.ScreenPriviliges = GetBestPrivilige(dto.ScreenPriviliges, list[i].ScreenPriviliges);
            }

            // Merge Element Priviliges.
            if (dto.ScreenPriviliges == RoleScreen.CUSTOM_PRIVILEGE)
            {
                var elementsList = new List<RoleScreenElementDTO>();
                foreach (var screen in from screen in list
                                       where screen.ScreenPriviliges == RoleScreen.CUSTOM_PRIVILEGE
                                       select screen)
                {
                    foreach (var (element, existingElement) in from element in screen.ScreenElementPriviliges
                                                               let existingElement = elementsList.Find(x => x.ScreenElementId == element.ScreenElementId)
                                                               select (element, existingElement))
                    {
                        if (existingElement != null)
                            existingElement.Priviliges = GetBestPrivilige(existingElement.Priviliges, element.Priviliges);
                        else
                            elementsList.Add(element);
                    }
                }

                dto.ScreenElementPriviliges = elementsList;
            }
            else //None or Admin
            {
                dto.ScreenElementPriviliges = null;
            }
            return dto;
        }

        /// <summary>
        /// Gets the best screen privilige.
        /// </summary>
        /// <param name="p1">The first privilige value.</param>
        /// <param name="p2">The second privilge value.</param>
        /// <returns>Privilige as int.</returns>
        private static int GetBestPrivilige(int p1, int p2)
        {
            return Math.Max(p1, p2);
        }
    }
}