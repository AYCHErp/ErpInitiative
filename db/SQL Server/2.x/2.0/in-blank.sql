-->-->-- src/Frapid.Web/Areas/InitiativeNepal/db/SQL Server/2.x/2.0/src/03.menus/menus.sql --<--<--
DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'InitiativeNepal'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'InitiativeNepal'
);

DELETE FROM core.menus
WHERE app_name = 'InitiativeNepal';


EXECUTE core.create_app 'InitiativeNepal', 'InitiativeNepal', 'Initiative Nepal', '1.0', 'MixERP Inc.', 'December 1, 2015', 'blue comments', '/dashboard/in', NULL;

EXECUTE core.create_menu 'InitiativeNepal', 'Tasks', 'Tasks', '', 'lightning', '';
EXECUTE core.create_menu 'InitiativeNepal', 'InitiativeNepalSetup', 'InitiativeNepal Setup', '/dashboard/in', 'configure', 'Tasks';

GO
DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'InitiativeNepal',
'{*}'
;

GO

