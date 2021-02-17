<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDBusinesses.aspx.cs" Inherits="EDP_Project.BDBusinesses" %>
<asp:Content ID="stylesheetss" ContentPlaceHolderID="stylesheets" runat="server">
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css" integrity="sha512-HK5fgLBL+xu6dm/Ii3z4xhlSUyZgTT9tuc/hSrtw6uzJOvgRr2a9jyxxT1ely+B+xFAmJKVSTbpM/CuL7qxO8w==" crossorigin="anonymous" />
	<style>
		table, table .btn-sm {
			font-size: 13px;
		}

		table > tbody > tr > td {
			vertical-align: middle !important;
		}

		table > thead > tr > th {
			border: 0px !important;
		}

		#roles-table > tbody > tr > td:nth-child(1) {
			cursor: pointer;
		}
	</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
	<div class="d-flex justify-content-between">
		<h2>My Businesses</h2>
		<div class="my-auto">
			<asp:HyperLink NavigateUrl="/BDAddBusiness.aspx" runat="server" CssClass="btn btn-secondary" role="button">Add <i class="fas fa-plus"></i></asp:HyperLink>
		</div>
	</div>
	<hr />
	<form runat="server">
		<asp:GridView ID="gv_businesses" runat="server"></asp:GridView>
		<asp:ListView ID="lv_businesses" runat="server">
			<LayoutTemplate>
				<table class="table" id="businesses">
					<thead>
						<tr>
							<th>Name</th>
							<th>Registration Number</th>
							<th>Type</th>
							<th>Website URL</th>
							<th>Approved</th>
							<th>Options</th>
						</tr>
					</thead>
					<tbody>
						<tr runat="server" id="itemPlaceholder" />
					</tbody>
				</table>
			</LayoutTemplate>
			<ItemTemplate>
				<tr runat="server">
					<td>
						<asp:Label ID="lbl_name" runat="server" Text='<%# Eval("Name") %>' />
					</td>
					<td>
						<asp:Label ID="lbl_regNum" runat="server" Text='<%# Eval("RegistrationNumber") %>' />
					</td>
					<td>
						<asp:Label ID="lbl_type" runat="server" Text='<%# Eval("Type") %>' />
					</td>
					<td>
						<a href="<%# Eval("Url") %>" target="_blank">URL <i class="fas fa-external-link-alt"></i></a>
					</td>
					<td>
						<asp:Label ID="Label1" runat="server" Text='<%# (bool)Eval("Verified") ? "Yes" : "No" %>' />
					</td>
					<td>
						<button type="button" class="btn btn-link btn-sm roles" id="<%# Eval("Id") %>">Roles</button>
						<asp:HyperLink NavigateUrl='<%# "~/BDEmployees.aspx?business=" + Eval("Id") %>' runat="server" CssClass="btn-sm btn btn-link">Employees</asp:HyperLink>
						<asp:HyperLink NavigateUrl='<%# "~/BDUpdateBusiness.aspx?business=" + Eval("Id") %>' runat="server" CssClass="btn-sm btn btn-link">Update</asp:HyperLink>
						<button type="button" class="btn btn-link btn-sm deleteBusiness" id="<%# Eval("Id") %>">Delete</button>
					</td>
				</tr>
			</ItemTemplate>
		</asp:ListView>
	</form>
	<div id="roles-modal" class="modal fade" tabindex="-1">
		<div class="modal-dialog modal-dialog-centered modal-xl">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title"></h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
					<div id="modal-body-add-role">
						<div class="form-group">
							<label>Role name</label>
							<input type="text" placeholder="Role Name" class="form-control" name="rolename" maxlength="30"/>
						</div>
						<button class="btn btn-primary btn-add-role" type="button">Add</button>
					</div>
					<hr />
					<span id="modal-body-message"></span>
					<div id="modal-body-roles-table">
						<div id="modal-body-spinner">
							Loading... 
							<div class="spinner-border spinner-border-sm" role="status">
								<span class="sr-only"></span>
							</div>
						</div>
						<table class="table" id="roles-table">
							<thead>
								<tr>
									<th>Name</th>
									<th>Employee Count</th>
									<th>Options</th>
								</tr>
							</thead>
							<tbody></tbody>
						</table>
					</div>
					
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
	<script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/js/all.min.js" integrity="sha512-UwcC/iaz5ziHX7V6LjSKaXgCuRRqbTp1QHpbOJ4l1nw2/boCfZ2KlFIqBUA/uRVF0onbREnY9do8rM/uT/ilqw==" crossorigin="anonymous"></script>
	<script>
		// very messy forgive me -bryan
		var businessId = "";

		var rolesTable = $('#roles-modal #roles-table');

		function showRolesTable() {
			rolesTable.show();
		}

		function hideRolesTable() {
			rolesTable.hide();
		}

		function clearModalBodyMessage() {
			var mbm = $('#roles-modal #modal-body-message');
			mbm.text('');
			mbm.hide();
		}

		function showModalBodyMessage(message) {
			var mbm = $('#roles-modal #modal-body-message');
			mbm.text(message);
			mbm.show();
		}

		$('.roles').click(e => {
			clearModalBodyMessage();
			cleanModal();

			var rolesModal = $('#roles-modal');
			rolesModal.modal('show');

			businessId = e.target.id;

			$(rolesModal).find('.modal-title').text('Business Roles');
			var rolesModal = $(rolesModal)
			var rolesTable = rolesModal.find('#roles-table');
			var rolesTableBodyRows = $('#roles-table tbody');

			var roleSpinner = rolesModal.find('#modal-body-spinner');

			rolesTable.hide();

			var requestOptions = {
				headers: {
					accept: 'application/json'
				}
			};

			fetch(`/api/businessrole?businessId=${e.target.id}`, requestOptions)
				.then(response => response.json())
				.then(async data => {
					roleSpinner.hide();
					console.log(data);

					if (data.length > 0)
						rolesTable.show();
					else {
						showModalBodyMessage('You currently don\'t have any roles, add one?');
						return;
					}
					
					await data.forEach(role => {
						// console.log(role);
						if (!role.Deleted)
							appendToTable(rolesTableBodyRows, role);
					})

					reloadDeleteRoleEvent();
					reloadUpdateRoleEvent();
				});
		});

		// when modal closes
		$('#roles-modal').on('hidden.bs.modal', e => {
			$('#roles-modal tbody').html('');
			$('#modal-body-spinner').show();
			$('#roles-modal #modal-body-spinner').show();
			hideRolesTable();
			clearModalBodyMessage();
			cleanModal();
		});

		// add new role
		$('.btn-add-role').click(e => {
			clearModalBodyMessage();
			var roleName = $('#roles-modal input[name=rolename]').val();
			var rolesTable = $('#roles-modal #roles-table tbody');
			if ((roleName.toString().trim().length != 0) && businessId.length != 0) {
				fetch('/api/businessrole', {
					method: 'POST',
					headers: {
						accept: 'application/json',
						'Content-Type': 'application/json'
					},
					body: JSON.stringify({
						Name: roleName.toString().trim(),
						BusinessId: businessId,
					})
				})
					.then(response => response.json())
					.then(role => {
						if (role != null) {
							createAlert('Role added!', 'success', 3000, '#modal-body-add-role');
							appendToTable(rolesTable, role);
							showRolesTable();

							cleanModal();

							reloadDeleteRoleEvent();
							reloadUpdateRoleEvent();
						}
					})
					.catch(err => {
						createErrorAlert('#modal-body-add-role');
					})                
			}
		})

		function createAlert(message, context, timeout, elemToAppend) {
			$(elemToAppend).prepend(`<div class="alert alert-${context} fade show" role="alert">${message}</div>`)

			setTimeout(function () {
				$(elemToAppend + ' .alert').alert('close');
			}, timeout)
		}

		function createErrorAlert(elemToAppend) {
			createAlert('An error has occured, try again later.', 'danger', 4500, elemToAppend);
		}

		function reloadDeleteRoleEvent() {
			// clear event first
			$('.deleteRole').off();

			$('.deleteRole').click(e => {
				var businessRoleId = e.target.id;
				// let's delete
				fetch(
					'/api/businessrole',
					{
						method: 'DELETE',
						body: JSON.stringify({
							Id: businessRoleId
						}),
						headers: {
							'Content-Type': 'application/json',
							accept: 'application/json'
						}
					}
				)
				.then(response => response.json())
				.then(async jResponse => {
					if (jResponse) {
						createAlert('Role deleted!', 'success', 3000, '#modal-body-add-role');
						await $(e.target).parentsUntil('tbody').remove();

						if ($('#roles-modal #roles-table tbody tr').length == 0) {
							showModalBodyMessage('You currently don\'t have any roles, add one?');
							hideRolesTable();
						}
					}
				})
                .catch(err => { createErrorAlert('#modal-body-roles-table'); })

			})
		}

		function reloadUpdateRoleEvent() {
			// clear event first
			$('.updateRole').off();

			$('.updateRole').click(e => {
				var businessRoleId = e.target.id;

				console.log(businessRoleId);

				$(`#${businessRoleId}-general`).hide();
				$(`#${businessRoleId}-update`).show();

				var table = $(e.target).parentsUntil('tbody');

				var tableRow = table.find('td');

                if ($(tableRow[0]).find('input').length == 0) {
                    tableRow[0].innerHTML = `<input class="form-control form-control-sm" type="text" name="new-role-name" placeholder="New Role Name" value="${tableRow[0].innerText}" />`;
                }

				reloadConfirmUpdateRoleEvent();
				reloadCancelUpdateRoleEvent();

				console.log(tableRow.length > 0 ? tableRow[0] : undefined);
			})

			$('#roles-table tbody tr td:nth-child(1)').click(e => {
				$(e.target).off();

				var row = $(e.target).parentsUntil('tbody');

				var tableRow = row.find('td');

				if ($(tableRow[0]).find('input').length == 0) {
                    tableRow[0].innerHTML = `<input class="form-control form-control-sm" type="text" name="new-role-name" placeholder="New Role Name" value="${tableRow[0].innerText}" />`;
                }

                var businessRoleId = row.find('td:nth-child(3)').find('button.deleteRole').attr('id');

                $(`#${businessRoleId}-general`).hide();
				$(`#${businessRoleId}-update`).show();

                reloadConfirmUpdateRoleEvent();
                reloadCancelUpdateRoleEvent();
            })
		}

		function reloadConfirmUpdateRoleEvent() {
			$('.confirmUpdateRole').off();

			$('.confirmUpdateRole').click(e => {
				var businessRoleId = e.target.id;
				var table = $(e.target).parentsUntil('tbody');

				var tableRow = table.find('td');
				var roleNameField = $(tableRow[0]).find('input');

				var roleNameFieldValue = roleNameField.val().toString().trim();

                if (roleNameFieldValue.length > 0) {
                    fetch(
                        '/api/businessrole',
                        {
                            method: 'PUT',
                            headers: {
                                accept: 'application/json',
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify({
                                Id: businessRoleId,
                                Name: roleNameFieldValue,
                                BusinessId: businessId
                            })
                        }
                    )
                    .then(response => response.json())
                    .then(response => {
						if (response) {
							$(tableRow[0]).html(encodeURIComponent(roleNameFieldValue));
							createAlert('Role name updated.', 'success', 3500, '#modal-body-roles-table');
                            reloadUpdateRoleEvent();
                            reloadConfirmUpdateRoleEvent();
						}

                        $(`#${businessRoleId}-general`).show();
                        $(`#${businessRoleId}-update`).hide();
                    })
                    .catch(err => {
                        createErrorAlert('#modal-body-roles-table');
                    })
				} else {
                    createAlert('New role name is empty.', 'warning', 3500, '#modal-body-roles-table');
                }

				tableRow[0].innerHTML = `<input class="form-control" type="text" name="new-role-name" placeholder="New Role Name" value="${tableRow[0].innerText}" />`;
			})
		}

		function reloadCancelUpdateRoleEvent() {
			$('.cancelUpdateRole').off();

			$('.cancelUpdateRole').click(e => {
				var businessRoleId = e.target.id;
				var table = $(e.target).parentsUntil('tbody');

				var tableRow = table.find('td');
				var roleNameField = $(tableRow[0]).find('input');

				$(`#${businessRoleId}-general`).show();
				$(`#${businessRoleId}-update`).hide();

				$(tableRow[0]).html(roleNameField.val());

				reloadUpdateRoleEvent();
                reloadConfirmUpdateRoleEvent();
                reloadCancelUpdateRoleEvent();
			})
		}

		function cleanModal() {
			$('#roles-modal input[name=rolename]').text('');
		}

		function appendToTable(tableBody, role) {
			tableBody.append(`
			<tr>
				<td style="width: 250px;">${encodeURIComponent(role.Name.toString().trim())}</td>
				<td>${role.EmployeeCount}</td>
				<td>
					<div id="${role.Id}-general">
						<button type="button" class="btn btn-sm btn-warning updateRole" id="${role.Id}">Update</button>
						<button type="button" class="btn btn-sm btn-danger deleteRole" id="${role.Id}">Delete</button>
					</div>
					<div id="${role.Id}-update" style="display: none;">
						<button type="button" class="btn btn-sm btn-success confirmUpdateRole" id="${role.Id}">Confirm</button>
						<button type="button" class="btn btn-sm btn-secondary cancelUpdateRole" id="${role.Id}">Cancel</button>
					</div>
				</td>
			</tr>
			`)
		}
    </script>
</asp:Content>
